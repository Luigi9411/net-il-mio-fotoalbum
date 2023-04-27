const loadPhotos = filter => getPhotos(filter).then(renderPhotos);
const getPhotos = title => axios
    .get('/api/photo', title ? { params: { title } } : {})
    .then(res => ({ photos: res.data, visible: true }));

const renderPhotos = ({ photos }) => {
    const noPhotos = document.querySelector("#no-photos");
    const loader = document.querySelector("#photos-loader");
    const photosCards = document.querySelector("#photos-cards");
    const photosFilter = document.querySelector("#photos-filter");

   

    if (photos && photos.length > 0) {
        photosFilter.classList.add("d-block");
        noPhotos.classList.add("d-none");
    }
    else { noPhotos.classList.remove("d-none"); }

    loader.classList.add("d-none");

    photosCards.innerHTML = photos.map(photoComponent).join('');
};
const initFilter = () => {
    const filter = document.querySelector("#photos-filter input");
    filter.addEventListener("input", (e) => loadPhotos(e.target.value))
};

const getPhoto = id => axios
    .get(`/api/photo/${id}`)
    .then(res => res.data);



const photoComponent = photo => `
 <li>
        <div class="card" style="width: 18rem;">
            <img src="${photo.url}" class="card-img-top" alt="" style="height: 12rem;">
                <div class="card-body">
                    <h4>
                        <a class="card-title ms-lg-3" href="/photo/detail/${photo.id}">${photo.title}</a>
                </h4>
                <p class="card-text ms-lg-3">${photo.description}</p>
        </div >
    </div >
</li>`;




// <Categories>

const loadCategories = () => getCategories().then(renderCategories);

const getCategories = () => axios
    .get("/api/category")
    .then(res => res.data);

const renderCategories = Categories => {
    const CategoriesSelection = document.querySelector("#categories");
    CategoriesSelection.innerHTML = Categories.map(categoryOptionComponent).join('');
}

const categoryOptionComponent = category => `
	<div class="flex gap">
		<input id="${category.id}" type="checkbox" />
		<label for="${category.id}">${category.title}</label>
	</div>`;

// </Categories>

// <CreateMessage>

const CreateMessage = message => axios
    .post("/Api/Photo", message)
    .then(res => console.log(res.data))
    .catch(e => renderErrors(e.response.data.errors));

const initMessageForm = () => {
    const form = document.querySelector('#message-form');
    const email = document.querySelector('#email');
    const text = document.querySelector('#message');

    form.addEventListener("submit", e => {
        e.preventDefault();

        const message = getMessageFromForm(form);
        CreateMessage(message);
        email.value = '';
        text.value = '';
    });
}

const getMessageFromForm = form => {
    const email = form.querySelector('#email').value;
    const text = form.querySelector('#message').value;
    return {
        email,
        text
    }
}



const renderErrors = errors => {
    const titleErrors = document.querySelector("#title-errors");
    const descriptionErrors = document.querySelector("#description-errors");

    titleErrors.innerText = errors.Name?.join("\n") ?? "";
    descriptionErrors.innerText = errors.Description?.join("\n") ?? "";
};