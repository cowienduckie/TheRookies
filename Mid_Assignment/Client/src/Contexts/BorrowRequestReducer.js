export const ADD_BOOK = "ADD BOOK TO REQUEST";
export const REMOVE_BOOK = "REMOVE BOOK FROM REQUEST";
export const CLEAR_REQUEST = "REMOVE ALL BOOKS FROM REQUEST";

const addBookToRequest = (bookId, state) => {
  const request = [...state.borrowRequest];
  const bookIndex = request.findIndex((id) => id == bookId);

  if (bookIndex < 0) {
    request.push(bookId);
  }

  return { ...state, borrowRequest: request };
};

const removeBookFromRequest = (bookId, state) => {
  const request = [...state.borrowRequest];
  const bookIndex = request.findIndex((id) => id == bookId);

  if (bookIndex >= 0) {
    request.splice(bookIndex, 1);
  }

  return { ...state, borrowRequest: request };
};

const clearRequest = (state) => {
  return { ...state, borrowRequest: [] };
};

export const borrowRequestReducer = (state, action) => {
  switch (action.type) {
    case ADD_BOOK:
      return addBookToRequest(action.bookId, state);

    case REMOVE_BOOK:
      return removeBookFromRequest(action.bookId, state);

    case CLEAR_REQUEST:
      return clearRequest(state);

    default:
      return state;
  }
};
