<template>
    <div>
        <p>부서 관리 뷰</p>
        <table>
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
        </table>
        <div>
            <b-button>Button</b-button>
            <b-button variant="danger">Button</b-button>
            <b-button variant="success">Button</b-button>
            <b-button variant="outline-primary">Button</b-button>
        </div>
    </div>
</template>
<script>
export default {
    data(){
        return {
            list:{}
        }
    },
    mounted(){
        this.getList()
    },
    methods:{
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
            .finally(()=>{
                console.log("finally");
            })
        }
    }
}
</script>