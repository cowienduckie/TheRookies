import { BASE_URL } from "../Constants/SystemConstants";
import { callApi } from "../Helpers/ApiHelper";

const url = `${BASE_URL}/api/books`;

export async function getBooks(queries = "") {
  return await callApi("get", url + queries);
}

export async function getBookById(id) {
  return await callApi("get", url + "/" + id);
}

export async function createBook(createModel) {
  return await callApi("post", url, createModel);
}

export async function updateBook(updateModel) {
  return await callApi("put", url, updateModel);
}

export async function deleteBook(id) {
  return await callApi("delete", url + "/" + id);
}
