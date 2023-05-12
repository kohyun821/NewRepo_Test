<template>
    <div>
        <b-table striped hover :items="list" :fields="fields">
            <thead>
                <th>부서코드</th>
                <th>부서명</th>
                <th>부서상태</th>
            </thead>
            <tbody>
                <tr v-for="(value, idx) in list" :key="idx">
                    <td>{{ value.departmentId }}</td>
                    <td>{{ value.departmentName }}</td>
                    <td>{{ value.departmentStatus }}</td>
                </tr>
            </tbody>
        </b-table>
        <div>
            <input v-model="dep" type="text"/>
            <b-button variant="success" @click="depAdd">부서 추가</b-button>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            dep:'부서명',
            list:{},
            fields:['departmentId','departmentName','departmentStatus']
        }
    },
    mounted(){
        this.getList()
    },
    methods:{
        depAdd(){
            this.axios
            .post('https://localhost:7059/Department/RegistDepartment',{departmentName:this.dep})
            .then((result)=>{
                console.log(result.data);
            })
            .catch((error)=>{
                console.log(error);
                alert('존재하는 부서명입니다.');
            })
        },
        getList(){
            this.axios
            .get('https://localhost:7059/Department')
            .then((result)=>{
                console.log(result.data);
                this.list = result.data;
            })
            .catch((error)=>{
                console.log(error);
            })
        }
    }
}
</script>