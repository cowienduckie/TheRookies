import App from './App';
import { createBrowserRouter } from 'react-router-dom';
import { AdminPage, AuthenticatePage, BookPage, BorrowBookPage, ErrorPage, HomePage } from "./Pages";
import { BookDetailsLoader, BookListLoader, BookListPage, BookPageLoader } from './Pages/BookPage';
import { BorrowBookAction, BorrowBookLoader } from './Pages/BorrowBookPage';
import { AuthenticateAction, AuthenticateLoader } from './Pages/AuthenticatePage';
import { AdminPageLoader, ViewBookListPage } from './Pages/AdminPage';
import { BookManageLoader, BookManagePage, CreateBookAction, CreateBookLoader, CreateBookPage, DeleteBookAction, UpdateBookAction, UpdateBookLoader, UpdateBookPage, ViewBookListLoader, ViewBookPage } from './Pages/AdminPage/BookManagePage';
import { CategoryManageLoader, CategoryManagePage, CreateCategoryAction, CreateCategoryLoader, CreateCategoryPage, DeleteCategoryAction, UpdateCategoryAction, UpdateCategoryLoader, UpdateCategoryPage, ViewCategoryListLoader, ViewCategoryListPage, ViewCategoryLoader, ViewCategoryPage } from './Pages/AdminPage/CategoryManagePage';
import { ApproveRequestAction, ApproveRequestLoader, ApproveRequestPage, BorrowRequestManageLoader, BorrowRequestManagePage, ViewRequestListLoader, ViewRequestListPage, ViewRequestLoader, ViewRequestPage } from './Pages/AdminPage/BorrowRequestManagePage';

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

const adminBookRouter = {
  path: "/admin/books",
  element: <BookManagePage />,
  loader: BookManageLoader,
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
    {
      path: "/admin/books/new",
      element: <CreateBookPage />,
      loader: CreateBookLoader,
      action: CreateBookAction
    },
    {
      path: "/admin/books/:bookId/edit",
      element: <UpdateBookPage />,
      loader: UpdateBookLoader,
      action: UpdateBookAction
    },
    {
      path: "/admin/books/:bookId/delete",
      action: DeleteBookAction
    }
  ]
}

const adminCategoryRouter = {
  path: "/admin/categories",
  element: <CategoryManagePage />,
  loader: CategoryManageLoader,
  children: [
    {
      index: true,
      element: <ViewCategoryListPage />,
      loader: ViewCategoryListLoader
    },
    {
      path: "/admin/categories/:categoryId",
      element: <ViewCategoryPage />,
      loader: ViewCategoryLoader
    },
    {
      path: "/admin/categories/new",
      element: <CreateCategoryPage />,
      loader: CreateCategoryLoader,
      action: CreateCategoryAction
    },
    {
      path: "/admin/categories/:categoryId/edit",
      element: <UpdateCategoryPage />,
      loader: UpdateCategoryLoader,
      action: UpdateCategoryAction
    },
    {
      path: "/admin/categories/:categoryId/delete",
      action: DeleteCategoryAction
    }
  ]
}

const adminBorrowRequestRouter = {
  path: "/admin/borrow-requests",
  element: <BorrowRequestManagePage />,
  loader: BorrowRequestManageLoader,
  children: [
    {
      index: true,
      path: "/admin/borrow-requests/list",
      element: <ViewRequestListPage />,
      loader: ViewRequestListLoader
    },
    {
      path: "/admin/borrow-requests/:requestId",
      element: <ViewRequestPage />,
      loader: ViewRequestLoader
    },
    {
      path: "/admin/borrow-requests/:requestId/approve",
      element: <ApproveRequestPage />,
      loader: ApproveRequestLoader,
      action: ApproveRequestAction
    }
  ]
}

const adminRouter = {
  path: "/admin",
  element: <AdminPage />,
  loader: AdminPageLoader,
  children: [
    adminBookRouter,
    adminCategoryRouter,
    adminBorrowRequestRouter
  ]
}

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      homeRouter,
      authenticateRouter,
      bookRouter,
      borrowBookRouter,
      adminRouter
    ]
  }
]);
