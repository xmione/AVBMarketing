import axios from "axios";

export default axios.create({
    baseURL: "https://localhost:7033/",
    headers: {
        "Content-type": "application/json"
    }
});