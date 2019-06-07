export const removeRowById = (list, id) => {
  const index = list.findIndex(x => x.id === id);
  if (index > -1) {
    list.splice(index, 1);
  }

  return list;
};

export const findById = (list, id) => list.find(x => x.id === id);

export const updateByObjectId = (list, updated) =>
  list.map(row => (row.id === updated.id ? updated : row));

export const sortIds = allRows =>
  allRows.sort(function(a, b) {
    return a.id - b.id || a.name.localeCompare(b.name);
  });

export const generateNewId = generateId =>
  generateId[generateId.length - 1].id + 1;
