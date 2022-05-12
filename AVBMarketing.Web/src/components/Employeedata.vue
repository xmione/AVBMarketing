<template>  
<div class="row" style="margin-bottom: 10px;">    
  <div class="col-sm-12 btn btn-success">    
   How to fetch data from WEB API in Vue.js Application  
  </div>    
</div>   
    <div>  
<table class="table">  
        <thead>  
        <th>Id</th>  
        <th>Name</th>  
        <th>City</th>  
        <th>Age</th>  
        <th>Technology</th>  
        <th></th>  
        </thead>  
        <tbody>  
            <tr v-for="item in employees" :key="item.Id">  
               <td>{{item.Id}}</td>  
                <td>{{item.Name }}</td>  
                 <td>{{item.City }}</td>  
                 <td>{{item.Age}}</td>  
                 <td>{{item.Tech}}</td>   
            </tr>  
        </tbody>  
    </table>  
    </div>  
</template>  
  
<script>  
import EemployeeService from "../Employeeservice";  
    export default {  
         name: 'Employeelist',  
             data() {  
       return {  
      employees: [],  
    };  
  },  
          created()  
        {  
             this.retrieveemployees();  
        },  
      
    methods: {  
        retrieveemployees() {  
      EemployeeService.getAll().then(response => {  
      this.employees = response.data;  
          console.log(response.data);  
        })  
        .catch(e => {  
          console.log(e);  
        });  
    },  
  
}  
}  
</script>  
  
<style lang="scss" scoped>  
  
</style>  