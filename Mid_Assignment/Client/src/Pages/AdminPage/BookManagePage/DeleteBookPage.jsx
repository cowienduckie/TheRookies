import { redirect } from "react-router-dom";
import { deleteBook } from "../../../Apis/BookApis";

export async function action({ params }) {
  await deleteBook(params.bookId);

  return redirect("/admin/books");
}
