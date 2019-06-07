import React, { Component } from "react";

class PersonFormAdd extends Component {
  state = {
    firstName: this.props.data === undefined ? "" : this.props.data.firstName,
    lastName: this.props.data === undefined ? "" : this.props.data.lastName,
    age: this.props.data === undefined ? 0 : this.props.data.age,
    hobby: this.props.data === undefined ? "" : this.props.data.hobby,
    id: this.props.data === undefined ? 0 : this.props.data.id,
    isActive: this.props.data === undefined ? "" : this.props.data.isActive
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
    return (
      <div className="modal-body">
        <form>
          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Firstname
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="firstName"
                  name="firstName"
                  value={this.props.firstName}
                  onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Lastname
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="lastName"
                  name="lastName"
                  value={this.props.lastName}
                  onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Age
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="number"
                  className="form-control"
                  placeholder="age"
                  name="age"
                  min="0"
                  max="100"
                  value={this.props.age}
                  onChange={this.props.handleChange}
                />
              </div>
            </div>
          </div>
          <div className="form-group">
            <label htmlFor="name" className="cols-sm-2 control-label">
              Hobby
            </label>
            <div className="cols-sm-5">
              <div className="input-group">
                <span className="input-group-addon">
                  <i className="fa fa-user fa" aria-hidden="true" />
                </span>
                <input
                  type="text"
                  className="form-control"
                  placeholder="hobby"
                  name="hobby"
                  value={this.props.hobby}
                  onChange={this.props.handleChange}
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

export default PersonFormAdd;
