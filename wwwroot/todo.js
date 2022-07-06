const uri = 'api/todos';
let todos = [];

function getItems(token) {
    if (token == null)
        token = JWTtoken;

  status = document.getElementById('listOfStatus').value.trim();
    fetch(uri + "/?status=" + status + "&token=" + JWTtoken, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + token,
        }
    })
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');
  const selectStatus = document.getElementById('statusSelect');
  const addDescription = document.getElementById('add-description');

  const item = {
    status: selectStatus.value.trim(),
      name: addNameTextbox.value.trim(),
      description: addDescription.value.trim()
  };

    fetch(uri + "?token=" + JWTtoken, {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + JWTtoken,
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
    .then(() => {
      getItems(JWTtoken);
      addNameTextbox.value = '';
      addDescription.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`+"?token="+JWTtoken, {
      method: 'DELETE',
      headers: {
        'Authorization': 'Bearer ' + JWTtoken,
      }
  })
  .then(() => getItems(JWTtoken))
  .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
  const item = todos.find(item => item.id === id);
  
  document.getElementById('edit-name').value = item.name;
  document.getElementById('edit-id').value = item.id;
  document.getElementById('edit-description').value = item.description;
  document.getElementById('edit-statusSelect').value = item.status;
  document.getElementById('editForm').style.display = 'block';

    //Date timeCreated = new Date();
    //Date timeUpdated = new Date();
    timeCreated = item.datecreated;
    timeUpdated = item.dateupdated;
  document.getElementById('edit-datecreated').value = item.datecreated;
  document.getElementById('edit-datecreated').innerHTML = "date created: " + timeCreated;
  document.getElementById('edit-dateupdated').value = item.dateupdated;
  document.getElementById('edit-dateupdated').innerHTML = "date updated: " + timeUpdated;
}

function updateItem() {
  const itemId = document.getElementById('edit-id').value;
  const item = {
    id: parseInt(itemId, 10),
    name: document.getElementById('edit-name').value.trim(),
    status: document.getElementById('edit-statusSelect').value.trim(),
    datecreated: document.getElementById('edit-datecreated').value.trim(),
    description: document.getElementById('edit-description').value.trim()
  };

  fetch(`${uri}/${itemId}`+"?token="+JWTtoken, {
    method: 'PUT',
    headers: {
        'Authorization': 'Bearer ' + JWTtoken,
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(item)
  })
  .then(() => getItems(JWTtoken))
  .catch(error => console.error('Unable to update item.', error));

  closeInput();

  return false;
}

function closeInput() {
  document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
  const name = (itemCount === 1) ? 'to-do' : 'to-dos';

  document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {

    let editButton = button.cloneNode(false);
    editButton.innerText = 'Edit';
    editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

    let deleteButton = button.cloneNode(false);
    deleteButton.innerText = 'Delete';
    deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
      let status = document.createTextNode(item.status);
      td1.appendChild(status);

    let td2 = tr.insertCell(1);
    let textNode = document.createTextNode(item.name);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    td3.appendChild(editButton);

    let td4 = tr.insertCell(3);
    td4.appendChild(deleteButton);
  });

  todos = data;
}