import axios from "axios";

export async function getRookies() {
  let rookiesList = [];

  await axios.get(`https://localhost:7069/rookies`)
    .then(result => {
      rookiesList = [...result.data];
    })
    .catch(error => {
      console.log(error);
    });

  return rookiesList;
}

export async function getRookieById(id) {
  let rookie = undefined;

  await axios.get(`https://localhost:7069/rookies/${id}`)
    .then(result => {
      rookie = result.data;
    })
    .catch(error => {
      console.log(error);
    })

  return rookie;
}

export async function createNewRookie(createModel) {
  let responseModel = undefined;

  await axios.post('https://localhost:7069/rookies', createModel)
    .then(result => {
      responseModel = result.data;
    })
    .catch(error => {
      console.log(error);
    })

  return responseModel;
}

export async function updateRookie(id, updateModel) {
  let responseModel = undefined;

  console.log(updateModel)

  await axios({
    method: 'put',
    url: `https://localhost:7069/rookies/${id}`,
    data: updateModel
  })
    .then(result => {
      responseModel = result.data;
    })
    .catch(error => {
      console.log(error);
    })

  console.log(responseModel);

  return responseModel;
}

export async function deleteRookie(id) {
  await axios.delete(`https://localhost:7069/rookies/${id}`)
    .catch(error => {
      console.log(error);
    })
}