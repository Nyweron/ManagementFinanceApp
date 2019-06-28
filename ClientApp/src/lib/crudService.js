export const createObject = (object, url) => {
  return fetch(url, {
    method: "POST",
    headers: {
      "Accept": "application/json",
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

export const deleteRow = (id, url) => {
  return fetch(url + "/" + id, {
    method: 'DELETE',
    headers: {
     "Access-Control-Allow-Origin": "*",
     "Access-Control-Allow-Methods": "GET, PUT, OPTIONS, POST, DELETE",
     "Access-Control-Allow-Headers": "Origin, X-Requested-With, Content-Type, Accept, Authorization"
    }
  }).then(() => {
     console.log('removed');
  }).catch(err => {
    console.error("XXX:"+err)
  })
}