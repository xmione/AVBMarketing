

<template>
    <container_tag>
        <div class="row" style="margin-bottom: 10px;">
            <div class="col-sm-12 btn btn-success">
                Overlapping Schedules
            </div>
        </div>
        <div>
            <table class="table">
                <thead>
                <th>Id</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th></th>
                </thead>
                <tbody>
                    <tr v-for="item in overlappingSchedules" :key="item.Id">
                        <td>{{item.Id}}</td>
                        <td>{{format_date(item.StartDate) }}</td>
                        <td>{{format_date(item.EndDate) }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </container_tag>
</template>  
  
<script>  
    import OverlappingScheduleService from "../OverlappingScheduleService";
    import moment from 'moment';

    export default {
        name: 'OverlappingScheduleList',  
        data()
        {
            return { overlappingSchedules: [], };  
        },  
        created()  
        {  
             this.RetrieveOverlappingSchedules();  
        },  
      
        methods:
        {
            RetrieveOverlappingSchedules()
            {
                OverlappingScheduleService.getAll().then(response =>
                {
                    this.overlappingSchedules = response.data;  
                    console.log(response.data);  
                })  
                .catch(e =>
                {
                    console.log(e);  
                });  
            },
            
            format_date(value) {
                if (value) {
                    return moment(String(value)).format("MMM ddd, yyyy hh:ss")
                }
            },
        }  
    }  
</script>  
  
<style lang="scss" scoped>  
  
</style>  