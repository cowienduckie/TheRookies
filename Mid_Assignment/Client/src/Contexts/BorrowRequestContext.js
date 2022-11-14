import { createContext } from "react";

export const BorrowRequestContext = createContext({
  borrowRequest: [],
  addBookToRequest: (bookId) => {},
  removeBookFromRequest: (bookId) => {},
  clearRequest: () => {},
});
