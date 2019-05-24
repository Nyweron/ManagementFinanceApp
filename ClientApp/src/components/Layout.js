import React, { Component } from 'react';
import { NavMenu } from './NavMenu';

export class Layout extends Component {

  render() {
    return (
      <div class="container-fluid">
        <div class="row">
          <div class="col-12">
            <NavMenu />
          </div>
        </div>
        <div class="row">
          <div class="col-12">
            {this.props.children}
          </div>
        </div>
      </div>
    );
  }
}
