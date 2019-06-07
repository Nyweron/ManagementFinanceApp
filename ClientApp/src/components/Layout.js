import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class Layout extends Component {

  render() {
    return (
      <div className="container-fluid">
        <div className="row">
          <div className="col-12">
            <NavMenu />
          </div>
        </div>
        <div className="row">
          <div className="col-12">
            {this.props.children}
          </div>
        </div>
      </div>
    );
  }
}
