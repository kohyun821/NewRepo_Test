<template>
    <div>
        <div>
            <b-button variant="success" @click="EmpAdd">사원 추가</b-button>
        </div>
        <b-table striped hover :items="list">
            <thead>
                <th></th>
                <th></th>
                <th></th>
            </thead>
            <tbody>
                <tr v-for="(value, idx) in list" :key="idx">
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </tbody>
        </b-table>
    </div>
</template>

<script>
export default {
    data(){
        return {
            formData:{
                departmentName:''
            },
            list:[]
        }
    },
    mounted(){
        this.getList()
    },
    methods:{
        getList(){
            this.axios
            .get('http://localhost:54884/api/employee/empList')
            .then((result)=>{
                console.log(result.data.list);
                this.list = result.data.list;
            })
            .catch((error)=>{
                console.log(error);
            })
        }
    }
}
</script>