import React, { Component } from "react";
import { Modal } from "../modal/Modal";
import PersonFormAdd from "./PersonFormAdd";

class TableAdd extends Component {
  showModal = () => {
    this.setState({ show: true });
  };

  hideModal = () => {
    this.setState({ show: false });
    this.props.negationAdd();
  };

  addForm = () => {
    this.showModal();
  };

  submitAddForm = () => {
    this.hideModal();
    this.props.handleSubmitAddRow(this.state);
  };

  handleChange = event => {
    event.preventDefault();
    this.setState({ [event.target.name]: event.target.value });
  };

  render() {
    if (this.props.show === false) {
      return null;
    }

    return (
      <Modal show={this.props.show}>
        <PersonFormAdd
          handleChange={this.handleChange}
          submitAddForm={this.submitAddForm}
          hideModal={this.hideModal}
          data={this.props.row}
        />
      </Modal>
    );
  }
}

export default TableAdd;
