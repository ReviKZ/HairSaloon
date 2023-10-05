/**
 * Universal fetch for data transfer
 * @param {string} type "get", "post", "put", "patch", "delete" are your options, depending what you want to do.
 * @param {string} endpoint The backend endpoint.
 * @param {any} payload Usually the body of a "post" or "put" request. For "delete", "patch" and "get" leave it blank.
 */
async function Fetch(type, endpoint, payload) {
    let response;

    if (type === "get") {
        response = await fetch(
            `${process.env.REACT_APP_API_URL}${endpoint}`, {
            method: "GET",
        }
        );
    }

    if (type === "post") {
        response = await fetch(
            `${process.env.REACT_APP_API_URL}${endpoint}`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(payload)
        }
        );
    }

    if (type === "put") {
        response = await fetch(
            `${process.env.REACT_APP_API_URL}${endpoint}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(payload)
        }
        );
    }

    if (type === "patch") {
        response = await fetch(
            `${process.env.REACT_APP_API_URL}${endpoint}`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(payload)
        }
        );
    }

    if (type === "delete") {
        response = await fetch(
            `${process.env.REACT_APP_API_URL}${endpoint}`, {
            method: "DELETE",
        }
        );
    }

    if (!response.ok) {
        return false;
    }

    return response.json();

}

export default Fetch;