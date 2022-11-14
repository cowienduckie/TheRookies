import { BASE_URL } from "../Constants/SystemConstants";
import { callApi } from "../Helpers/ApiHelper";

const url = `${BASE_URL}/api/borrow-requests`;

export async function getBorrowRequests(queries = "") {
  return await callApi("get", url + queries);
}

export async function getBorrowRequestById(id) {
  return await callApi("get", url + "/" + id);
}

export async function createBorrowRequest(createModel) {
  return await callApi("post", url, createModel);
}

export async function approveBorrowRequest(approveModel) {
  return await callApi("post", url + "/approval", approveModel);
}
