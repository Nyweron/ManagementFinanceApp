import React from "react";
import "./modal.css";

export const Modal = props => {
  const showHideClassName = props.show
    ? "modal display-block"
    : "modal display-none";

  return (
    <div className={showHideClassName}>
      <section className="modal-main">{props.children}</section>
    </div>
  );
};
