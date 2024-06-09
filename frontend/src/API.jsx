import URI from "./backendConfig";

const getBase64 = (file) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });


const API=(uri,options)=>{
    console.log(URI+uri)
    console.log(options)
    const call=fetch(URI+uri,options);
    return call;
}

export {getBase64}
export default API