import React, { Component } from 'react';
import TableContainer from '../container/TableContainer';

export class Expense extends Component {
  displayName = Expense.name
  render() {
    return (
      <div className="row">
        <div className="col-12">
          <TableContainer />
        </div>
    </div>
    );
  }
}
