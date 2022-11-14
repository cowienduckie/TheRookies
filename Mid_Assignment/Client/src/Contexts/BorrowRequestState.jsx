import { useReducer } from "react";
import { BorrowRequestContext } from "./BorrowRequestContext";
import {
  ADD_BOOK,
  borrowRequestReducer,
  CLEAR_REQUEST,
  REMOVE_BOOK,
} from "./BorrowRequestReducer";

export function BorrowRequestState(props) {
  const [requestState, dispatch] = useReducer(borrowRequestReducer, {
    borrowRequest: [],
  });

  const addBookToRequest = (bookId) => {
    dispatch({ type: ADD_BOOK, bookId: bookId });
  };

  const removeBookFromRequest = (bookId) => {
    dispatch({ type: REMOVE_BOOK, bookId: bookId });
  };

  const clearRequest = () => {
    dispatch({ type: CLEAR_REQUEST });
  };

  return (
    <BorrowRequestContext.Provider
      value={{
        borrowRequest: requestState.borrowRequest,
        addBookToRequest: addBookToRequest,
        removeBookFromRequest: removeBookFromRequest,
        clearRequest: clearRequest,
      }}
    >
      {props.children}
    </BorrowRequestContext.Provider>
  );
}
