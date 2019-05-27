import { render, cleanup, fireEvent } from "react-testing-library";
import React from "react";
import "jest-dom/extend-expect";
import { TableRow } from "../components/table/TableRow";

// automatically unmount and cleanup DOM after the test is finished.
afterEach(cleanup);

const keys = ["id", "firstName", "lastName", "age", "isActive", "hobby"];

const rows = [
  {
    id: 1,
    firstName: "Crystal",
    lastName: "Andrews",
    age: 72,
    isActive: true,
    hobby: "id voluptate non qui aliquip laboris mollit"
  },
  {
    id: 2,
    firstName: "Jennings",
    lastName: "Wade",
    age: 56,
    isActive: false,
    hobby: "occaecat irure incididunt sit excepteur excepteur aliqua"
  }
];

describe("TableRow", () => {
  it("find id=2 from rows", () => {
    const { getByTestId } = render(
      <table>
        <thead>
          <TableRow keys={keys} rows={rows} />
        </thead>
        <tbody />
      </table>
    );

    const elemRow = getByTestId("2-0");
    expect(elemRow.innerHTML).toBe("2");
    const elemRowFirstName = getByTestId("2-1");
    expect(elemRowFirstName.innerHTML).toBe("Jennings");
  });

  test("clicks handleRemove tag 'a' ", () => {
    const handleRemove = jest.fn();
    const { getByText } = render(
      <table>
        <thead>
          <TableRow keys={keys} rows={rows} handleRemove={handleRemove} />
        </thead>
        <tbody />
      </table>
    );

    fireEvent.click(getByText("X"));
    expect(handleRemove).toHaveBeenCalledTimes(1);
  });
});
