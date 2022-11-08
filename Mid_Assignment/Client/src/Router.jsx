import App from './App';
import { createBrowserRouter } from 'react-router-dom';
import { AdminPage, AuthenticatePage, BookPage, BorrowBookPage, ErrorPage, HomePage } from "./Pages";
import { BookDetailsLoader, BookListLoader, BookListPage, BookPageLoader } from './Pages/BookPage';
import { BorrowBookAction, BorrowBookLoader } from './Pages/BorrowBookPage';
import { AuthenticateAction, AuthenticateLoader } from './Pages/AuthenticatePage';
import { AdminPageLoader, ViewBookListPage } from './Pages/AdminPage';
import { ViewBookListLoader, ViewBookPage } from './Pages/AdminPage/BookManagePage';

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      homeRouter,
      authenticateRouter,
      bookRouter,
      borrowBookRouter
    ]
  }
]);

const homeRouter = {
  index: true,
  element: <HomePage />
}

const authenticateRouter = {
  path: "/authenticate",
  element: <AuthenticatePage />,
  loader: AuthenticateLoader,
  action: AuthenticateAction
}

const bookRouter = {
  path: "/books",
  element: <BookPage />,
  loader: BookPageLoader,
  children: [
    {
      index: true,
      path: "/books/list",
      element: <BookListPage />,
      loader: BookListLoader
    },
    {
      path: "books/:bookId",
      element: <BookDetailsLoader />,
      loader: BookDetailsLoader
    }
  ]
}

const borrowBookRouter = {
  path: "/borrow-books",
  element: <BorrowBookPage />,
  loader: BorrowBookLoader,
  action: BorrowBookAction
}

const adminRouter = {
  path: "/admin",
  element: <AdminPage />,
  loader: AdminPageLoader,
  children: [
    {
      index: true,
      path: "/admin/books",
      children: [
        {
          index: true,
          path: "/admin/books/list",
          element: <ViewBookListPage />,
          loader: ViewBookListLoader
        },
        {
          path: "/admin/books/:bookId",
          element: <ViewBookPage />,
          loader: ViewBookPage
        },
      ]
    }
  ]
}
