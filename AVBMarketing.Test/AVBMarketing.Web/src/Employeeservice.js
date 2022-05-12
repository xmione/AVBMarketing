import http from "./Http-comman";
class EemployeeService {

    getAll() {
        return http.get("/api/EmployeeInfo/Getemployeeinfo");
    }

}
export default new EemployeeService();