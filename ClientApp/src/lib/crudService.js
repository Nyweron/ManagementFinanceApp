export const createObject = (object, url) => {
  console.log("object",object);
  console.log("JSON.stringify(object)",JSON.stringify(object));
  return fetch(url, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(object)
  })
  .then(response => {
      if (response.status !== 201) {
        console.log('Looks like there was a problem. Status Code: ' + response.status);
        return;
      }
    })
  .catch(error => console.error(error));
};