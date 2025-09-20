export function initUserPickerSearchbox() {
    const debounceDelay = 300;
    let debounceTimeout;

    $(document).on('input', '.partial-user-picker-searchbox', function () {
        clearTimeout(debounceTimeout);

        const input = $(this);
        debounceTimeout = setTimeout(() => {
            const wrapper = input.closest('.position-relative');
            const dataList = $(`#${input.attr('list')}`);
            const hiddenInput = wrapper.find('input[type="hidden"]');
            const spinner = wrapper.find('#spinner-' + wrapper.data('instance-id'));

            const searchQuery = input.val().trim();

            if (!searchQuery) {
                dataList.empty();
                hiddenInput.val('');
                spinner.addClass('d-none');
                return;
            }

            spinner.removeClass('d-none');

            $.ajax({
                url: '/Users/SearchUserByName',
                method: 'GET',
                data: { searchQuery },
                success: function (response) {
                    dataList.empty();
                    response.userProfiles.forEach(userProfile => {
                        dataList.append(`<option data-id="${userProfile.id}" value="${userProfile.firstNameLastName}"></option>`);
                    });
                },
                error: function () {
                    dataList.empty();
                },
                complete: function () {
                    spinner.addClass('d-none');
                }
            });
        }, debounceDelay);
    });

    $(document).on('change', '.partial-user-picker-searchbox', function () {
        const input = $(this);
        const wrapper = input.closest('.position-relative');
        const dataList = $(`#${input.attr('list')}`);
        const hiddenInput = wrapper.find('input[type="hidden"]');
        const typedValue = input.val();

        const matchedOption = dataList.find(`option[value="${typedValue}"]`);

        if (matchedOption.length > 0) {
            hiddenInput.val(matchedOption.data('id'));
        } else {
            hiddenInput.val('');
        }
    });
}
