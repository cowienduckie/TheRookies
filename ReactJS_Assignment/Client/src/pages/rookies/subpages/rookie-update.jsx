import { redirect, useLoaderData } from "react-router-dom";
import { updateRookie } from "../../../apis/rookies";
import { RookieForm } from "../../../components/form/RookieForm";

export async function action({ request, params }) {
  const formData = await request.formData();
  const rookie = Object.fromEntries(formData);

  var response = await updateRookie(params.rookieId, rookie);

  if (!response) {
    throw new Response("", {
      status: 400,
      statusText: 'Bad Request'
    })
  }
  
  return redirect(`/rookies/${response.id}`);
}

export function RookieUpdatePage() {
  const rookie = useLoaderData()

  return <>
    <RookieForm
      path={`/rookies/${rookie.id}/edit`}
      method='put'
      title='UPDATE ROOKIE'
      data={rookie} />
  </>
}