<template>
    <div class="about">
        <h1>자재관리</h1>
        <table v-if="materials.length">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>이름</th>
                    <th>남은 양</th>
                    <th>총 합</th>
                    <th>상태</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="material in materials" :key="material.materialId">
                    <td>{{ material.materialId }}</td>
                    <td>{{ material.materialName }}</td>
                    <td>{{ material.materialAmount }}</td>
                    <td>{{ material.materialTotal }}</td>
                    <td>{{ material.materialStatus ? '활성화' : '비 활성화' }}</td>
                </tr>
            </tbody>
        </table>
        <div v-else>
            <p>No materials found.</p>
        </div>
    </div>
    <h1>
        1515asdf
    </h1>
    <div>
        <form @submit.prevent="CreateMaterial">
            <div>
                <label>이름</label>
                <input type="text" id="materialName" v-model="FormData.materialName" />
            </div>
            <div>
                <label>수량</label>
                <input type="number" id="materialAmount" v-model="FormData.materialAmount" />
            </div>
            <div>
                <label>합계</label>
                <input type="number" id="materialTotal" v-model="FormData.materialTotal" />
            </div>
            <button>생성</button>
        </form>

    </div>
</template>

<script>
import axios from 'axios'
export default {
    name: 'CreateMaterial',
    data() {
        return {
            materials: [],
            FormData: {
                materialName: '',
                materialAmount: '',
                materialTotal: ''
            }
        }
    },
    async created() {
        try {
            const response = await axios.get('https://localhost:7059/Material')
            console.log(response.data);
            this.materials = response.data
        } catch (error) {
            console.error(error)
        }
    },
    methods:{
        CreateMaterial(){
            axios.post("")
        }
    },
}
</script>
