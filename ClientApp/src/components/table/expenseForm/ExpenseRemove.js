import React, { Component } from "react";

class ExpenseRemove extends Component {

  acceptRemove = () => {
    this.props.hideModalRemove();
    this.props.handleRemove()
  };

  render() {
    if (this.props.isRemove === false) {
      return null;
    }
    return (
      <div className="modal-body">
        <div className="form-group">
          <label htmlFor="name" className="cols-sm-2 control-label">
            Do you want really remove this?
          </label>
        </div>
        <button onClick={this.acceptRemove}>yes</button>
        <button onClick={this.props.hideModalRemove}>no</button>
      </div>
    );
  }
}

export default ExpenseRemove;
