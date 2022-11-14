import { redirect, useLoaderData } from "react-router-dom";
import { getAllCategories } from "../../../Apis/CategoryApis";
import { createBook } from "../../../Apis/BookApis";
import { BookForm } from "../../../Components/Form/BookForm";

export async function loader() {
  const blankBook = {
    name: "",
    description: "",
    cover: "",
    categoryIds: [],
  };

  const categories = await getAllCategories();

  return { blankBook, categories };
}

export async function action({ request }) {
  const formData = await request.formData();
  const newBook = Object.fromEntries(formData);

  newBook.categoryIds = [...newBook.categoryIds.split(",")];

  const createdBook = await createBook(newBook);

  return redirect(`/admin/books/${createdBook.id}`);
}

export function CreateBookPage() {
  const { blankBook, categories } = useLoaderData();

  return (
    <BookForm
      path="/admin/books/new"
      method="post"
      title="CREATE NEW BOOK"
      data={blankBook}
      allCategories={categories}
    />
  );
}
