import { redirect, useLoaderData } from "react-router-dom";
import { getAllCategories } from "../../../Apis/CategoryApis";
import { getBookById, updateBook } from "../../../Apis/BookApis";
import { BookForm } from "../../../Components/Form/BookForm";

export async function loader({ params }) {
  const book = await getBookById(params.bookId);
  const categories = await getAllCategories();

  return { book, categories };
}

export async function action({ params, request }) {
  const formData = await request.formData();
  const book = Object.fromEntries(formData);

  book.id = params.bookId;
  console.log(book);
  book.categoryIds = [...book.categoryIds.split(",")];

  const updatedBook = await updateBook(book);

  return redirect(`/admin/books/${updatedBook.id}`);
}

export function UpdateBookPage() {
  const { book, categories } = useLoaderData();

  return (
    <BookForm
      path={`/admin/books/${book.id}/edit`}
      method="post"
      title="UPDATE BOOK"
      data={book}
      allCategories={categories}
    />
  );
}
