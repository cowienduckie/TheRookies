import { redirect } from "react-router-dom";
import { deleteCategory } from "../../../Apis/CategoryApis";

export async function action({ params }) {
  await deleteCategory(params.categoryId);

  return redirect("/admin/categories");
}