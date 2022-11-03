import { redirect } from "react-router-dom";
import { deleteRookie } from "../../../apis/rookies";

export async function action({ params }) {
  await deleteRookie(params.rookieId);
  
  return redirect('/rookies');
}