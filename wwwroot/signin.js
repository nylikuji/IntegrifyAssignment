const signinurl = 'api/signin';
let JWTtoken = "";

function signIn() {

    const signinemail = document.getElementById('signin-email').value.trim();
    const signinpassword = document.getElementById('signin-password').value.trim();

    if (signinemail.length > 0 && signinpassword.length > 0) {
        fetch(signinurl + "?email=" + signinemail + "&password=" + signinpassword, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
        .then(response => response.json())
            .then(data => { console.log(data), JWTtoken = data, getItems(data) })
        .then(() => {
            document.getElementById('signin-email').value = "";
            document.getElementById('signin-password').value = "";
        })
        .catch(error => console.error('Unable to add item.', error));
    }
}