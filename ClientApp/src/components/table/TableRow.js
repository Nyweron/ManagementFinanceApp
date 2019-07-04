import React from "react";
import PropTypes from "prop-types";
import TableEdit from "./TableEdit";

export const TableRow = props => {
  if (props.rows === undefined || props.rows === null || props.rows.length === 0) {
    return null;
  }

  console.log("TableRow");

  let rowsToReturn = props.rows.map(row => {
    return (
      <tr style={{ height: "" }} key={row.id}>
        {props.keys.map((key, i) => {
          if (row[key] !== undefined && row[key] !== null) {
            return (
              <td data-testid={row.id + "-" + i} key={row.id + "-" + i}>
                {row[key].toString()}
              </td>
            );
          }
          return <td data-testid={row.id + "-" + i} key={row.id + "-" + i} />;
        })}
        <td className="delete-item">
          <p style={{ color: "blue", cursor: "default" }} onClick={() => props.handleRemove(row.id, true)}>
            X
          </p>
        </td>
        <td className="edit-item">
          <TableEdit row={row} handleEdit={props.handleEdit} />
        </td>
      </tr>
    );
  });

  return rowsToReturn;
};

TableRow.propTypes = {
  rows: PropTypes.array.isRequired,
  keys: PropTypes.array.isRequired
};
