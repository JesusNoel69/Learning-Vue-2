<template>
    
    <div class="users">
      <button v-on:click="isShowed=!isShowed; GetUsers()">
        <p v-if="isShowed">Hide users</p>
        <p v-else>Show users</p>
      </button>

      <div>
        <input v-model="idSelected" type="number"/>
        <button v-on:click="GetUser(idSelected)" ><!--showUser(idSelected)-->
            Find user
        </button>
        <p>{{ user }}</p>
      </div>
      <ul v-if="isShowed">
        <li  v-for="user in users" :key="user.id" 
            v-on:click="idSelected=user.id ">
            {{ user.name }} - {{ user.email }} 
        </li>
      </ul>

      <div>
        <br>
        <label>name </label>
        <input v-model="newUser.name" type="text">
        <br>
        <label>email </label>
        <input v-model="newUser.email" type="email">
        <br>
        <button v-on:click="CreateUser()">Create</button>
      </div>

      <div>
        <br>
        <label>id </label>
        <input v-model="updateUser.id" type="number">
        <br>
        <label>name </label>
        <input v-model="updateUser.name" type="text">
        <br>
        <label>email </label>
        <input v-model="updateUser.email" type="email">
        <br>
        <button v-on:click="UpdateUser(updateUser.id)">Update</button>
      </div>

      <div>
        <br>
        <label>id </label>
        <input v-model="idToDelete" type="number">
        <button v-on:click="DeleteUser(idToDelete)">Delete</button>
      </div>
    </div>
  </template>

  <script>
  export default {
    name: 'UserComponent',
    data() {
      return {
        users: [],
        idSelected : null,
        idToDelete:null,
        isShowed : false,
        user : {},
        newUser:{ name:'', email:''},
        updateUser: {id:null, name:'', email:''}
      };
    },
    methods: {
        async GetUsers() {
        try {
            const res = await fetch('http://localhost:5192/api/Users');
            if (!res.ok) {
            throw new Error("Error en la respuesta de la API");
            }
            this.users = await res.json();
        } catch (error) {
            console.error("Problema con el fetch:", error);
        }
        },
            showUser(id) {
        this.user = this.users.find(e=>e.id==id);
        },
        async GetUser(id) {
        try {
            const res = await fetch('http://localhost:5192/api/Users/'+id);
            if (!res.ok) {
            throw new Error("Error en la respuesta de la API");
            }
            this.user = await res.json();
        } catch (error) {
            console.error("Problema con el fetch:", error);
        }
        },
        async CreateUser() {
            if (this.newUser.name !== '' && this.newUser.email !== '') {
                try {
                    const res = await fetch('http://localhost:5192/api/Users/', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify({
                            Id: 0,
                            Name: this.newUser.name,
                            Email: this.newUser.email
                        })
                    });
                    console.log(this.newUser);
                    if (!res.ok) {
                        const errorText = await res.text();
                        throw new Error(`Error en la respuesta de la API: ${errorText}`);
                    }

                    const newUser = await res.json();
                    this.user = newUser;
                    console.log("Usuario creado exitosamente:", newUser);
                } catch (error) {
                    console.error("Problema con el fetch:", error);
                }
            }else{
                console.log("campos vacios");
            }
        },
        async UpdateUser(id) {
            try {
                const res = await fetch('http://localhost:5192/api/Users/' + id, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        Id: this.updateUser.id,
                        Name: this.updateUser.name,
                        Email: this.updateUser.email
                    })
                });

                if (!res.ok) {
                    const errorText = await res.text();
                    throw new Error(`Error en la respuesta de la API: ${errorText}`);
                }

                let updateUser;
                if (res.status !== 204) {
                    updateUser = await res.json();
                    console.log("Usuario actualizado exitosamente:", updateUser);
                } else {
                    console.log("Usuario actualizado.");
                }
            } catch (error) {
                console.error("Problema con el fetch:", error);
            }
        },
        async DeleteUser(id) {
            try {
                const res = await fetch('http://localhost:5192/api/Users/' + id, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                    }
                });
                if (!res.ok) {
                    const errorText = await res.text();
                    throw new Error(`Error en la respuesta de la API: ${errorText}`);
                }

                let updateUser;
                if (res.status !== 204) {
                    updateUser = await res.json();
                    console.log("Usuario eliminado exitosamente:", updateUser);
                } else {
                    console.log("Usuario eliminado.");
                }
            } catch (error) {
                console.error("Problema con el fetch:", error);
            }
        }
    },

    created(){
        this.GetUsers();
    }
  };
  </script>
  
  <style scoped>
    .users {
        border: 1px solid red;
        height: auto;
    }
    li{
        list-style-type: none;
    }
    p{
        margin: 0;
    }
  </style>