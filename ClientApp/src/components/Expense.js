import React, { Component } from 'react';
import TableContainer from '../container/TableContainer';

export class Expense extends Component {
  displayName = Expense.name

  render() {
    return (
      <div class="row">
        <div class="col-12">
          <TableContainer />
        </div>
    </div>
    );
  }
}
