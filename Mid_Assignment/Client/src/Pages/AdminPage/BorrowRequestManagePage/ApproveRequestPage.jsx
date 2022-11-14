import { redirect } from "react-router-dom";
import { approveBorrowRequest } from "../../../Apis/BorrowRequestApis";

export function loader() {}

export async function action({ params, request }) {
  const formData = await request.formData();
  const approveModel = Object.fromEntries(formData);

  approveModel.id = parseInt(params.requestId);
  approveModel.isApproved = approveModel.isApproved === "true";

  await approveBorrowRequest(approveModel);

  return redirect(`/admin/borrow-requests/${approveModel.id}`);
}

export function ApproveRequestPage() {
  return <></>;
}
