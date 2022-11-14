import { redirect, useLoaderData } from "react-router-dom";
import { createCategory } from "../../../Apis/CategoryApis";
import { CategoryForm } from "../../../Components/Form/CategoryForm";

export function loader() {
  return {
    name: "",
  };
}

export async function action({ request }) {
  const formData = await request.formData();
  const newCategory = Object.fromEntries(formData);

  await createCategory(newCategory);

  return redirect("/admin/categories/");
}

export function CreateCategoryPage() {
  const data = useLoaderData();

  return (
    <CategoryForm
      path="/admin/categories/new"
      method="post"
      title="CREATE NEW CATEGORY"
      data={data}
    />
  );
}
