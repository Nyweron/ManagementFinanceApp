import React, { Component } from "react";

class ExpenseAdd extends Component {
  state = {
    id: this.props.data === undefined ? "" : this.props.data.id,
  };

  handleChange = event => {
    event.preventDefault();
    this.setState({ [event.target.name]: event.target.value });
  };

  saveAdd = () => {
    this.props.hideModal();
    this.props.submitAddForm();
  };

  render() {
    if (this.props.show === false) {
      return null;
    }
    return (
      <div className="modal-body">
        <form>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Kwota How much
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="howMuch"
                  name="howMuch"
                  value={this.props.howMuch}
                  onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            Na co categoryExpenseId list
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="comment"
                  // name="comment"
                  // value={this.props.comment}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            Czym zapłacono categorySavingId list
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="comment"
                  // name="comment"
                  // value={this.props.comment}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Kiedy Date
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="howMuch"
                  // name="howMuch"
                  // value={this.props.howMuch}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>


          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            Kto userId list
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="comment"
                  // name="comment"
                  // value={this.props.comment}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            comment
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="comment"
                  name="comment"
                  value={this.props.comment}
                  onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            attachment
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="comment"
                  // name="comment"
                  // value={this.props.comment}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>

          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
            standingOrder checkbox
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  // className="form-control"
                  // placeholder="comment"
                  // name="comment"
                  // value={this.props.comment}
                  // onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>


        </form>

        <button onClick={this.saveAdd}>Save row</button>
        <button onClick={this.props.hideModal}>Close</button>
    </div>
    );
  }
}

export default ExpenseAdd;
