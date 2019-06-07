import React from "react";
import PropTypes from "prop-types";

export const TableHead = props => {
  return (
    <tr style={{ color: "blue", cursor: "default" }}>
      {props.keys.map(item => (
        <th key={item}>
          <span data-testid={item} onClick={() => props.sortColumn(item)}>
            {item}
          </span>
        </th>
      ))}
      <th>Remove</th>
      <th>Edit</th>
    </tr>
  );
};

TableHead.propTypes = {
  keys: PropTypes.array.isRequired
};
