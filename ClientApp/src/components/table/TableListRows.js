import React from "react";
import PropTypes from "prop-types";
import { TableRow } from "./TableRow";
import { TableHead } from "./TableHead";

export const TableListRows = props => {
  if(props.rows === undefined || props.rows === null || props.rows.length === 0) {
    return null;
  }

  console.log("TableListRows", props);

  return (
    <table className={props.classCss}>
      <thead>
        <TableHead sortColumn={props.sortColumn} keys={props.keys} />
      </thead>
      <tbody>
        <TableRow
          rows={props.rows}
          keys={props.keys}
          handleRemove={props.handleRemove}
          handleEdit={props.handleEdit}
        />
      </tbody>
    </table>
  );
};

TableListRows.propTypes = {
  rows: PropTypes.array.isRequired,
  keys: PropTypes.array.isRequired
};
