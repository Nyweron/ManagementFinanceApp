import { personUrl } from "../apiUrl";

export const getAll = () => {
  return fetch(personUrl).then(res => res.json());
};

export const getKeyFromJson = rows => {
  if (rows !== null && rows.length > 0) {
    return Object.keys(rows[0]);
  }
  return rows;
};

export const createPerson = person => {
  return fetch(personUrl, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(person)
  }).then(res => res.json());
};

export const deleteRow = id => {
  return fetch(`${personUrl}/${id}`, {
    method: "DELETE",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    }
  });
};

export const filterTable = (keys, rows, route, isSort) => {
  if (keys === null || keys === undefined || keys.length === 0) {
    return rows;
  }

  const keysLength = keys.length;
  for (let i = 0; i < keysLength; i++) {
    if (keys[i] === route) {
      return rows.sort(function(current, next) {
        let x = current[keys[i]];
        let y = next[keys[i]];

        if (typeof x === "string") {
          x = x.toUpperCase();
        }
        if (typeof y === "string") {
          y = y.toUpperCase();
        }

        if (isSort) {
          return sortDescending(x, y);
        } else {
          return sortAscending(x, y);
        }
      });
    } /*END if (keys[i] === route)*/
  } /*END for*/
  return rows;
};

function sortDescending(x, y) {
  if (x > y || y === undefined) {
    return -1;
  } else if (x < y || x === undefined) {
    return 1;
  } else {
    return 0;
  }
}

function sortAscending(x, y) {
  if (x < y || x === undefined) {
    return -1;
  } else if (x > y || y === undefined) {
    return 1;
  } else {
    return 0;
  }
}

export const updateRow = row => {
  return fetch(`${personUrl}/${row.id}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(row)
  }).then(res => res.json());
};
