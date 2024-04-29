const http = require("http");
const fs = require("fs");

const PORT = process.env.PORT || 4000;

let books = JSON.parse(fs.readFileSync("books.json"));
let users = JSON.parse(fs.readFileSync("users.json")); 

const server = http.createServer((request, response) => {
    if (request.url === "/users/signup" && request.method === "POST") {
        let body = "";
        request.on ("data", (chunk) => {
            body += chunk.toString();
        });
        request.on("end", () => {
            const newUser = JSON.parse(body);
            users.push(newUser);
            response.writeHead(200, {"Content-Type" : "application/json"});
            response.end(JSON.stringify(newUser));
        });
    } else {
        response.writeHead(404, { "Content-Type" : "text/plain"})
        response.end(
            JSON.stringify({
                message: "Invalid Endpoint",
                status: "failed"
            })
        );
    }
})

server.listen(PORT, () => {
    console.log(`The Server is active on Port ${PORT}.`);
});