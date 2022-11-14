import { redirect, useLoaderData } from "react-router-dom";
import { getCategoryById, updateCategory } from "../../../Apis/CategoryApis";
import { CategoryForm } from "../../../Components/Form/CategoryForm";

export async function loader({ params }) {
  return await getCategoryById(params.categoryId);
}

export async function action({ params, request }) {
  const formData = await request.formData();
  const category = Object.fromEntries(formData);

  category.id = params.categoryId;

  await updateCategory(category);

  return redirect("/admin/categories");
}

export function UpdateCategoryPage() {
  const data = useLoaderData();

  return (
    <CategoryForm
      path={`/admin/categories/${data.id}/edit`}
      method="put"
      title="UPDATE CATEGORY"
      data={data}
    />
  );
}
