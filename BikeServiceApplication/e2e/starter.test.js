describe('Example', () => {
  beforeAll(async () => {
    await device.launchApp();
  });

  beforeEach(async () => {
    await device.reloadReactNative();
  });

  it('test failed', async () => {
    await expect(element(by.name('test'))).toBeVisible();
  });
});
