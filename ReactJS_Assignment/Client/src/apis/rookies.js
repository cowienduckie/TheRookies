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

  console.log(rookiesList);

  return rookiesList;
}