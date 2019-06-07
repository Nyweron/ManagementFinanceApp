import React, { Component } from 'react';
import { managementFinance } from "../apiUrls";

export class FetchData extends Component {
  displayName = FetchData.name

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };

    fetch(managementFinance +'api/expense/')
      .then(response => response.json())
      .then(data => {
        this.setState({ forecasts: data, loading: false });
      });
  }

  static renderForecastsTable(forecasts) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>id</th>
            <th>howMuch</th>
            <th>date</th>
            <th>categorySavingId</th>
            <th>categoryExpenseId</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map(forecast =>
            <tr key={forecast.id}>
              <td>{forecast.id}</td>
              <td>{forecast.howMuch}</td>
              <td>{forecast.date}</td>
              <td>{forecast.categorySavingId}</td>
              <td>{forecast.categoryExpenseId}</td>
            </tr>

          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1>Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
}
