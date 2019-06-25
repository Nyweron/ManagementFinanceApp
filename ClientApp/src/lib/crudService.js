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
  .then(res => res.json())
  .catch(error => console.error(error));
};