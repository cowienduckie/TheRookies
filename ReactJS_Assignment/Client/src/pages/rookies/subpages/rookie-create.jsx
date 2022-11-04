import { redirect, useLoaderData } from "react-router-dom";
import { createNewRookie } from "../../../apis/rookies";
import { RookieForm } from "../../../components/form/RookieForm";

export async function action({ request }) {
  const formData = await request.formData();
  const newRookie = Object.fromEntries(formData);

  var response = await createNewRookie(newRookie);

  if (!response) {
    throw new Response("", {
      status: 400,
      statusText: 'Bad Request'
    })
  }
  
  return redirect(`/rookies/${response.id}`);
}

export async function loader() {
  return {
    firstName: '',
    lastName: '',
    gender: '',
    dateOfBirth: '',
    birthPlace: ''
  }
}

export function RookieCreatePage() {
  const rookie = useLoaderData()

  return <>
    <RookieForm
      path='/rookies/new'
      method='post'
      title='CREATE NEW ROOKIE'
      data={rookie} />
  </>
}