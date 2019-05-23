import {
  removeRowById,
  findById,
  sortIds,
  generateNewId,
  updateByObjectId
} from "./personHelpers";

test("findById should find object by id from list", () => {
  const startPersons = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    }
  ];
  const id = 1;
  const expected = {
    id: 1,
    firstName: "Tom",
    lastName: "Cat",
    age: 10,
    isActive: true,
    hobby: "Bike"
  };

  const result = findById(startPersons, id);
  expect(result).toEqual(expected);
});

test("removeRowById should remove one element from array by id", () => {
  const startPersons = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    },
    {
      id: 3,
      firstName: "Duck",
      lastName: "Donald Duck",
      age: 12,
      isActive: false,
      hobby: "Play tennis"
    }
  ];
  const id = 2;
  const expected = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 3,
      firstName: "Duck",
      lastName: "Donald Duck",
      age: 12,
      isActive: false,
      hobby: "Play tennis"
    }
  ];

  const result = removeRowById(startPersons, id);
  expect(result).toEqual(expected);
});

test("sortIds should return sorted items ascending", () => {
  const startPersons = [
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    },
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 4,
      firstName: "Bunny",
      lastName: "Bugs Bunny",
      age: 13,
      isActive: false,
      hobby: "Eat carrots"
    },
    {
      id: 3,
      firstName: "Duck",
      lastName: "Donald Duck",
      age: 12,
      isActive: false,
      hobby: "Play tennis"
    }
  ];
  const expected = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    },
    {
      id: 3,
      firstName: "Duck",
      lastName: "Donald Duck",
      age: 12,
      isActive: false,
      hobby: "Play tennis"
    },
    {
      id: 4,
      firstName: "Bunny",
      lastName: "Bugs Bunny",
      age: 13,
      isActive: false,
      hobby: "Eat carrots"
    }
  ];

  const result = sortIds(startPersons);
  expect(result).toEqual(expected);
});

test("generateNewId should return new id number ascending ", () => {
  const startPersons = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    }
  ];
  const expectedId = 3;

  const result = generateNewId(startPersons);
  expect(result).toEqual(expectedId);
});

test("updateByObjectId should update exist object with new data", () => {
  const startPersons = [
    {
      id: 1,
      firstName: "Tom",
      lastName: "Cat",
      age: 10,
      isActive: true,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    }
  ];
  const updatedObject = {
    id: 1,
    firstName: "Updated Tom",
    lastName: "Updated Cat",
    age: 10,
    isActive: false,
    hobby: "Bike"
  };

  const expected = [
    {
      id: 1,
      firstName: "Updated Tom",
      lastName: "Updated Cat",
      age: 10,
      isActive: false,
      hobby: "Bike"
    },
    {
      id: 2,
      firstName: "Jerry",
      lastName: "Mouse",
      age: 11,
      isActive: true,
      hobby: "Sleep"
    }
  ];

  const result = updateByObjectId(startPersons, updatedObject);
  expect(result).toEqual(expected);
});
