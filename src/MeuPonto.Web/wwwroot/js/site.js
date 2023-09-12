// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$.fn.exists = function () {
    return this.length !== 0;
}

const $formModal = $('#formModal');

const $formModalContent = $formModal.find('.modal-content');

var bsFormModal;

var formModalContentTemplate;

var $form;

if ($formModal.exists()) {
    bsFormModal = new bootstrap.Modal($formModal, {})
}

const $modalAnchors = $('a[target=_modal]');

$(async () => {
    formModalContentTemplate = $formModalContent.html();
});

$modalAnchors.on('click', async (e) => {
    const $modalAnchor = $(e.currentTarget);

    e.preventDefault();

    const url = $modalAnchor.attr('href');

    const title = $modalAnchor.attr('title');

    $formModalContent.html(formModalContentTemplate);

    const $formModalTitle = $formModal.find('.modal-title');

    if (title == undefined) {
        const titleAlt = $modalAnchor.text().trim();

        $formModalTitle.text(titleAlt);
    }
    else {
        $formModalTitle.text(title);
    }

    bsFormModal.show();

    const content = await $.ajax({
        type: "GET",
        url: url,
        data: { display: 'modal' }
    });

    $formModalContent.html(content);

    $form = $formModalContent.find('form');

    $form.attr('action', url);

    //    const $commands = $formModalContent.find('.Command');

    //    const $formModalFooter = $formModal.find('.modal-footer');

    //    $formModalFooter.prepend($commands);

    const onSubmit = async (e) => {
        const submitter = e.originalEvent.submitter;

        const frm = e.currentTarget;

        e.preventDefault();

        $formModalContent.html(formModalContentTemplate);

        var content;

        try {
            var formData = new FormData(frm);

            formData.append(submitter.name, submitter.value);

            const response = await fetch(url, {
                method: 'POST',
                body: formData
            });

            if (response.redirected) {
                window.location.href = response.url;
            }
            else {
                content = await response.text();
            }            
        } catch (e) {
            
        }

        $formModalContent.html(content);

        const displayInput = $formModalContent.find('input[name=display]');

        if (displayInput.val() == 'modal') {
            const $f = $formModalContent.find('form');

            $f.attr('action', url);

            $f.on('submit', onSubmit);
        }
        else {
            console.log(displayInput);
        }
    };

    $form.on('submit', onSubmit);
});
