const passwordchangeuri = 'api/changepassword';

function changePassword() {

    const newpassword = document.getElementById('newpassword').value.trim();

    if (newpassword.length > 0) {
        fetch(passwordchangeuri + "?newpass=" + newpassword + "&token=" + JWTtoken, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + JWTtoken,
            },
        })
        .then(response => response.json())
            //.then(data => { console.log(data) })
        .then(() => {
            document.getElementById('newpassword').value = "";
        })
        .catch(error => console.error('Unable to add item.', error));
    }
}
