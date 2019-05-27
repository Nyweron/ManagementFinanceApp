import { render, cleanup, fireEvent } from "react-testing-library";
import React from "react";
import "jest-dom/extend-expect";
import { TableHead } from "../components/table/TableHead";

// automatically unmount and cleanup DOM after the test is finished.
afterEach(cleanup);

const keys = ["header 1", "header 2", "header 3", "header 4"];

describe("TableHead", () => {
  it("find first key from keys", () => {
    const { getByTestId } = render(
      <table>
        <thead>
          <TableHead keys={keys} />
        </thead>
        <tbody />
      </table>
    );
    const elem = getByTestId("header 1");
    expect(elem.innerHTML).toBe("header 1");
  });

  test("clicks sortColumn", () => {
    const sortColumn = jest.fn();
    const { getByText } = render(
      <table>
        <thead>
          <TableHead keys={keys} sortColumn={sortColumn} />
        </thead>
        <tbody />
      </table>
    );

    fireEvent.click(getByText("header 1"));
    expect(sortColumn).toHaveBeenCalledTimes(1);
  });
});
