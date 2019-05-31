import React, { Component } from "react";
import { TableListRows } from "../components/table/TableListRows";
import ExpenseAdd from "../components/table/expenseForm/ExpenseAdd";
import Pagination from "../components/pagination/Pagination";
import { Modal } from "../components/modal/Modal";
import {
  getAll,
  getKeyFromJson,
  filterTable,
  createPerson,
  deleteRow,
  updateRow
} from "../lib/personService";
import {
  removeRowById,
  updateByObjectId,
  sortIds,
  generateNewId
} from "../lib/personHelpers";
import { createObject } from "../lib/crudService";
import { apiUrlExpense } from "../apiUrls";


export class Expense extends Component {
  displayName = Expense.name;

  state = {
    rowsFromDbJson: [],
    keysFromDbJson: [],
    sort: true,
    columnName: "",
    previousColumnName: "",
    add: false,
    message: "",
    currentRows: [],
    currentPage: 1,
    pageLimit: 5,
    pageNeighbours: 5
  };

  componentDidMount() {
    getAll().then(rows => {
      this.setState({ rowsFromDbJson: rows });
      const keys = getKeyFromJson(rows);
      if (keys !== null) {
        this.setState({ keysFromDbJson: keys });
      }
    });
  }
  handleRemove = id => {
    let listOfRows = this.state.rowsFromDbJson;
    const newListWithoutRemovedItem = removeRowById(listOfRows, id);

    deleteRow(id).then(
      () => this.showTempMessage("row deleted"),
      this.setState({ rowsFromDbJson: newListWithoutRemovedItem }, () => {
        this.invokePaginationOnPageChanged();
      })
    );
  };

  invokePaginationOnPageChanged = () => {
    const data = {};
    data.totalRecords = this.state.rowsFromDbJson.length;
    data.pageLimit = this.state.pageLimit;
    data.pageNeighbours = this.state.pageNeighbours;
    data.currentPage = this.state.currentPage;
    this.onPageChanged(data);
  };

  negationAdd = () => {
    this.setState({ add: !this.state.add });
  };

  showTempMessage = msg => {
    this.setState({ message: msg });
    setTimeout(() => {
      this.setState({ message: "" });
    }, 2000);
  };

  handleSubmitAddRow = addObj => {
    if (
      addObj.id === ""
      // addObj.firstName === null ||
      // addObj.firstName === undefined ||
      // addObj.firstName === ""
    ) {
      this.showTempMessage("Firstname is required");
      return;
    }

    const allRows = this.state.rowsFromDbJson;
    const sortedIds = sortIds(allRows);
    if (sortedIds.length === 0) {
      sortedIds.push("");
    }
    const newId = generateNewId(sortedIds);

    const newObj = {
      //Id: newId,
      Comment: "TEST234"
    };

    createObject(newObj, apiUrlExpense).then(
      () => this.showTempMessage("objcet created"),
      this.setState(
        {
          rowsFromDbJson: [...this.state.rowsFromDbJson, newObj]
        },
        () => {
          this.invokePaginationOnPageChanged();
        }
      )
    );

    for (var key in addObj) {
      delete addObj[key];
    }
  };

  handleEdit = editObj => {
    let listOfRows = this.state.rowsFromDbJson;

    const editExistRow = {
      id: editObj.id,
      firstName: editObj.firstName,
      lastName: editObj.lastName,
      age: editObj.age,
      isActive: true,
      hobby: editObj.hobby
    };

    const newUpdatedRowList = updateByObjectId(listOfRows, editExistRow);

    updateRow(editExistRow).then(
      () => this.showTempMessage("row updated"),
      this.setState(
        {
          rowsFromDbJson: newUpdatedRowList
        },
        () => {
          this.invokePaginationOnPageChanged();
        }
      )
    );
  };

  sortColumn = currentColumnName => {
    /* We use 2 because in list always will be empty row with id=0 and new row which we will create. */
    if (this.state.rowsFromDbJson.length === 2) {
      return;
    }
    if (this.state.previousColumnName === currentColumnName) {
      this.setState({ columnName: currentColumnName });
      this.setState(prevState => ({
        sort: !prevState.sort
      }));
    } else {
      this.setState({
        columnName: currentColumnName,
        previousColumnName: currentColumnName
      });
      this.setState(prevState => ({
        sort: !prevState.sort
      }));
    }
  };

  onPageChanged = data => {
    console.log("data", data);
    const offset = (data.currentPage - 1) * data.pageLimit;
    const currentRows = this.state.rowsFromDbJson.slice(
      offset,
      offset + data.pageLimit
    );

    this.setState({
      currentPage: data.currentPage,
      rowsFromDbJson: this.state.rowsFromDbJson,
      currentRows
    });
  };

  showModal = () => {
    this.setState({ show: true });
  };

  hideModal = () => {
    this.setState({ show: false });
    this.negationAdd();
  };

  addForm = () => {
    this.showModal();
  };

  submitAddForm = () => {
    this.hideModal();
    this.handleSubmitAddRow(this.state);
  };

  handleChange = event => {
    event.preventDefault();
    this.setState({ [event.target.name]: event.target.value });
  };


  render() {
    if (this.state.rowsFromDbJson.length === 0) {
      return null;
    }

    const displayTable = filterTable(
      this.state.keysFromDbJson,
      this.state.currentRows,
      this.state.columnName,
      this.state.sort
    );


    return (
      <div className="row">
        <div className="col-12">
          <div className="">
            <button className="btn" onClick={this.negationAdd}>
              Add row
            </button>
            {this.state.message && (
              <span className="success">{this.state.message}</span>
            )}
          </div>

          <Modal show={this.state.add}>
            <ExpenseAdd
              show={this.state.add}
              handleSubmitAddRow={this.handleSubmitAddRow}
              handleChange={this.handleChange}
              negationAdd={this.negationAdd}
              expenseAdd={this.expenseAdd}
              hideModal={this.hideModal}
              submitAddForm={this.submitAddForm}
            />
          </Modal>

          {/* <TableAdd
            show={this.state.add}
            handleSubmitAddRow={this.handleSubmitAddRow}
            handleChange={this.handleChange}
            negationAdd={this.negationAdd}
            expenseAdd={this.expenseAdd}
          /> */}


        </div>

        <div className="col-12">
          <TableListRows
            rows={displayTable}
            keys={
              this.state.keysFromDbJson === null
                ? null
                : this.state.keysFromDbJson
            }
            classCss="table table-striped table-bordered"
            handleChange={this.handleChange}
            sortColumn={this.sortColumn}
            handleRemove={this.handleRemove}
            handleEdit={this.handleEdit}
          />
        </div>

        <div className="col-12" >
          <div className="d-flex flex-row py-4 align-items-center justify-content-center">
            <Pagination
              totalRecords={this.state.rowsFromDbJson.length}
              pageLimit={this.state.pageLimit}
              pageNeighbours={this.state.pageNeighbours}
              onPageChanged={this.onPageChanged}
            />
          </div>
        </div>
    </div>


    );
  }
}
